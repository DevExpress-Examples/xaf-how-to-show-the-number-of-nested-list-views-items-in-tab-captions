namespace DetailViewTabCount.Module.Helpers
{
    public static class DetailViewControllerHelper
    {
        public static string ClearItemCountInTabCaption(string caption)
        {
            int index = caption.IndexOf('(');
            if (index != -1)
            {
                return caption.Remove(index - 1);
            }
            return caption;
        }
    }
}
