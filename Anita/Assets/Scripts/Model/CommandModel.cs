namespace Anita
{
    public class CommandModel : AVGModel
    {
        private ImageModel imageModel; //角色立绘模型
        private string background_music; //背景音乐文件名
        private string background_image; //背景图片文件名

        public override string ToString()
        {
            return " CommandModel[ " + imageModel.ToString() + " bk_image: " +
                background_image + " bk_music: " + background_music + "] ";
        }

        public CommandModel(string image = null, int pos = 1, int x_offset = 0, int y_offset = 0,
            string background_image = null, string background_music = null)
        {
            this.background_image = background_image;
            this.background_music = background_music;
            if (image != null)
            {
                imageModel = new ImageModel(image, pos, x_offset, y_offset);
            }
        }

        public class ImageModel
        {
            private string image; //立绘文件名
            private int pos; //立绘位置 0-居左 1-居中 2-居右
            private int x_offset; //x偏移
            private int y_offset; //y偏移

            public override string ToString()
            {
                return "ImageModel[ image: " + image + " pos: " + pos + " xOffset: "
                    + x_offset + " yOffset: " + y_offset + "] ";
            }

            public ImageModel(string image, int pos, int x_offset, int y_offset)
            {
                this.image = image;
                this.pos = pos;
                this.x_offset = x_offset;
                this.y_offset = y_offset;
            }

            public string Image { get => image; set => image = value; }
            public int Pos { get => pos; set => pos = value; }
            public int X_offset { get => x_offset; set => x_offset = value; }
            public int Y_offset { get => y_offset; set => y_offset = value; }
        }

        public string Background_music { get => background_music; set => background_music = value; }
        public string Background_image { get => background_image; set => background_image = value; }
        public ImageModel ImageModel0 { get => imageModel; set => imageModel = value; }
    }
}