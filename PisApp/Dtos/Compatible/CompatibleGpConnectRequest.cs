namespace PisApp.API.Compatibles.Dtos
{
    public class CompatibleGpConnectRequestDto
    {
        public required int gpu_id          { get; set; }

        public required int power_supply_id { get; set; }
    }
}