namespace WebMvc.Models
{
    public class OrderProcessAction
    {
        public static OrderProcessAction Ship = new OrderProcessAction(nameof(Ship).ToLowerInvariant(), "Ship");

        public string Code { get; private set; }
        public string Name { get; private set; }


        protected OrderProcessAction()
        {
        }

        public OrderProcessAction(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
