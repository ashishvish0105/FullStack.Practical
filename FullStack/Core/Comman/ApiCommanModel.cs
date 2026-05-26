namespace FullStack.Core.Comman
{
    public class ApiCommanModel
    {
        public int statusCode{ get; set; }
        public string message { get; set; }
        public dynamic payload{ get; set; }
        public Guid requestId { get; set; }

        public ApiCommanModel()
        {
            statusCode = StatusCodes.Status400BadRequest;
            payload = null;
            requestId = Guid.NewGuid();
            message = "Bad request";
        }

        public ApiCommanModel(string message)
        {
            statusCode = StatusCodes.Status400BadRequest;
            payload = null;
            requestId = Guid.NewGuid();
            message = message;
        }

        public static ApiCommanModel apiRespons(int statusCode,string message, dynamic payload)
        {
            return new ApiCommanModel
            {
                statusCode = statusCode,
                payload = payload,
                message = message,
                requestId = Guid.NewGuid()
            };
        }

    }
}
