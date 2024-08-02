using Microsoft.AspNetCore.Components;

namespace PennStateSoft
{
    public class UIHelper
    {
        [Parameter]
        public int Rows { get; set; }

        public void CalculateSize(string value)
        {
            Rows = Math.Max(value.Split('\n').Length, value.Split('\r').Length);
            Rows = Math.Max(Rows, 3);
        }
    }
}
