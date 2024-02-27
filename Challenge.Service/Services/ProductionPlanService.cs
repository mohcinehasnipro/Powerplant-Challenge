using Challenge.Service.Dtos;
using Challenge.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Challenge.Service.Services
{
    public class ProductionPlanService : IProductionPlanService
    {
        private readonly ILogger<ProductionPlanService> _logger;

        public ProductionPlanService(ILogger<ProductionPlanService> logger)
        {
            _logger = logger;
        }
        public List<Response> Calculate(Payload payload)
        {
            // Initialize the result production plan
            var responses = new List<Response>();

            try
            {
                var sortedPowerPlants = payload.Powerplants.ToList();

                // Sort power plants by capacity (merit order)
                sortedPowerPlants.Sort((x, y) => y.Pmax.CompareTo(x.Pmax));

                // Initialize remaining load
                double remainingLoad = payload.Load;

                responses = CalculatePowerProduction(sortedPowerPlants, remainingLoad, payload.Fuels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return responses;
        }

        private double GetProductionCost(PowerPlant powerPlant, Payload payload)
        {
            if (powerPlant.Type == "gasfired")
            {
                // Calculate cost based on gas price and efficiency
                double gasCost = payload.Fuels.GasEuroMWh * (1 / powerPlant.Efficiency);

                // double co2Cost = payload.Fuels.Co2EuroTon / (1 / powerPlant.Efficiency);

                return gasCost;
            }
            else if (powerPlant.Type == "turbojet")
            {
                // Calculate cost based on kerosene price and efficiency
                double keroseneCost = payload.Fuels.KerosineEuroMWh * (1 / powerPlant.Efficiency);
                // double co2Cost = payload.Fuels.Co2EuroTon / (1 / powerPlant.Efficiency);
                return keroseneCost;
            }
            else
                return 0;
        }

        private static List<Response> CalculatePowerProduction(List<PowerPlant> powerPlants, double load, Fuels fuels)
        {
            var production = new List<Response>();

            // Calcul de la production d'énergie pour chaque centrale électrique
            foreach (var plant in powerPlants)
            {
                double plantProduction = 0;

                if (plant.Type == "gasfired")
                {
                    double nombreUnites = 1 / plant.Efficiency;
                    double maxGasProduction = plant.Pmin * nombreUnites;
                    plantProduction = Math.Min(load, maxGasProduction) * fuels.GasEuroMWh;
                }
                else if (plant.Type == "turbojet")
                {
                    double nombreUnites = 1 / plant.Efficiency;
                    double maxGasProduction = plant.Pmin * nombreUnites;
                    plantProduction = Math.Min(load, maxGasProduction) * fuels.KerosineEuroMWh;
                }
                else if (plant.Type == "windturbine")
                {
                    double maxWindProduction = plant.Pmax * fuels.Wind;
                    plantProduction = Math.Min(load, maxWindProduction) / 100;
                }

                // Assurer que la production est un multiple de 0.1 MW
                plantProduction = Math.Round(plantProduction / 0.1) * 0.1;

                production.Add(new Response { Name = plant.Name, P = plantProduction });
                load -= plantProduction;
            }

            return production;
        }

    }

}
