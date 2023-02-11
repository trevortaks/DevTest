namespace DevTest.Shared
{
    public class ResponseModel
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }

    }

    public class ResponseModel<T> : ResponseModel
    {
        public ResponseModel(T content, bool success = true, string message = "")
        {
            Content = content;
            Message = message;
            Success = success;
        }

        public T Content { get; set; }
    }
}
