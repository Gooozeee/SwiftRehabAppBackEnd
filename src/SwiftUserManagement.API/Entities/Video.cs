namespace SwiftUserManagement.API.Entities
{
    // Entity which is used for showing where the video data is stored
    public class Video
    {
        public int Video_Id { get; set; }
        public int User_Id { get; set; }
        public string FilePath { get; set; }
        public string Prediction { get; set; }
    }
}
