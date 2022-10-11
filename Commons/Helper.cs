namespace StudentManagementApp.Commons
{
    public static class Helper
    {
        public static string GenerateCode(int id)
        {
            DateTimeOffset dto = new DateTimeOffset(DateTime.UtcNow);
            string date = dto.ToUnixTimeSeconds().ToString();

            return $"SCH-{id.ToString("0000")}-{date}";
        }

        public static int SelectEnum(string screenMessage, int validStart, int validEnd)
        {
            int outValue;
            do
            {
                Console.Write(screenMessage);
            } while (!(int.TryParse(Console.ReadLine(), out outValue) && IsValid(outValue, validStart, validEnd)));
            return outValue;
        }

        public static bool IsValid(int outValue, int start, int end)
        {
            return outValue >= start && outValue <= end;
        }
    }
}
