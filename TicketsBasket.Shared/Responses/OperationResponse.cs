namespace TicketsBasket.Shared.Responses
{
    public class OperationResponse<T>
    {
        public string Message { get; set; }
        public T Record { get; set; }
    }
}