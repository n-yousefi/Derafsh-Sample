namespace DerafshSample.ModelsLibrary.ViewModels.General
{
    public class IdentityResult
    {
        public bool Succeeded { get; set; }
        public string Code { get; set; }
        public virtual object Model { get; set; }
        public string Description { get; set; }

        public static IdentityResult Success => new IdentityResult()
        {
            Succeeded = true,
            Description = "Information was submited successfully."
        };

        public static IdentityResult SubmitFailed(string description = "") => new IdentityResult()
        {
            Succeeded = false,
            Description = string.IsNullOrEmpty(description) ? "Sorry, an error occurred while saving information!" : description
        };

        public static IdentityResult FetchFailed(string description = "") => new IdentityResult()
        {
            Succeeded = false,
            Description = string.IsNullOrEmpty(description) ? "Sorry, an error occurred while retrieving information!" : description
        };
    }
}
