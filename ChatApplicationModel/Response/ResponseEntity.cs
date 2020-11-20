namespace ChatApplicationModel.Response
{
    using System.Net;
    public class ResponseEntity
    {
        public HttpStatusCode httpStatusCode { get; set; }

        public string message { get; set; }

        public object data { get; set; }

        public string token { get; set; }

    }
}
