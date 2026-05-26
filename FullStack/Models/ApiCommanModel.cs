namespace FullStack.Models
{
    public class ApiCommanModel
    {
        public int statusCode{ get; set; }
        public string messgae { get; set; }
        public dynamic payload{ get; set; }
        public Guid requestId { get; set; }

        public ApiCommanModel()
        {
            this.statusCode = StatusCodes.Status400BadRequest;
            this.payload = null;
            requestId = Guid.NewGuid();
            messgae = "Bad request";
        }

        public ApiCommanModel(string message)
        {
            this.statusCode = StatusCodes.Status400BadRequest;
            this.payload = null;
            requestId = Guid.NewGuid();
            messgae = message;
        }

        public static ApiCommanModel apiRespons(int statusCode,string message, dynamic payload)
        {
            return new ApiCommanModel
            {
                statusCode = statusCode,
                payload = payload,
                messgae = message,
                requestId = Guid.NewGuid()
            };
        }

    }
}
