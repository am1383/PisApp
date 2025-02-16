namespace PisApp.API.Compatibles.Dtos
{
    public class CompatibleGmSlotRequest
    {
        public required int motherboard_id { get; set; }
        
        public required int gpu_id { get; set; }
    }
}