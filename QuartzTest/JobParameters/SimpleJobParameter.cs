namespace QuartzTest.JobParameters
{
    public class SimpleJobParameter
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public override string ToString()
        {
            return $"[Username: '{Username}', Password='{Password}']";
        }
    }
}
