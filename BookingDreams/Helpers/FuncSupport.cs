using System.Drawing;

namespace BookingDreams.Helpers
{
    public class FuncSupport
    {
        public bool IsImageFile(IFormFile file)
        {
            try
            {
                using (var img = Image.FromStream(file.OpenReadStream()))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
