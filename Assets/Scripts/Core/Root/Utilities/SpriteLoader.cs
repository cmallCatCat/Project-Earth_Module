using System.IO;
using UnityEngine;

namespace Core.Root.Utilities
{
    public static class SpriteLoader
    {
        public static Sprite LoadSprite(string picturePath)
        {
            using FileStream fs = new FileStream(picturePath, FileMode.Open, FileAccess.Read);
            using BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((int)fs.Length);
            Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            texture.LoadImage(bytes);
            texture.Apply();
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0,
                texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
    }
}
