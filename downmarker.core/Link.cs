namespace DownMarker.Core
{
    public class Link
    {
        public string Text { get; set; }
        public string Target { get; set; }

        public string ToMarkDown()
        {
            return "[" + this.Text + "](" + this.Target + ")";
        }
    }

}
