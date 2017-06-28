namespace Catan.Client.PresentationLayer
{
    public class SiedlungEventArgs
    {
        public SiedlungTexture ClickedSiedlung { private set; get; }

        public SiedlungEventArgs(SiedlungTexture siedlung)
        {
            this.ClickedSiedlung = siedlung;
        }
    }
}