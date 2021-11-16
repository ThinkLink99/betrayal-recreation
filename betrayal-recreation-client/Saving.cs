using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;
using Newtonsoft.Json;
namespace CSOTH_API
{
    public partial class Game
    {
        public void Save(string location, string savename)
        {
            // Save ground tiles
            if (!Directory.Exists(location))
                Directory.CreateDirectory(location);
            using (StreamWriter writer = new StreamWriter(File.Create(location + "\\" + savename + ".json")))
            {
                // write the opening brace of the json file
                writer.WriteLine("{");

                writer.WriteLine("\"pack\": \"" + packName + "\",");
                writer.WriteLine("\"CPI\":" + CurrentPlayerIndex + ",");

                // Save Players
                writer.WriteLine("\"players\": [");
                foreach (var player in Players)
                    if(player == Players.Last())
                        writer.WriteLine(player.ExportJSON());
                    else writer.WriteLine($"{player.ExportJSON()}, ");

                // close player array
                writer.WriteLine("],");

                // write array for tiles
                writer.WriteLine("\"BasementTiles\": [");
                foreach (var tile in BasementTiles)
                    if (tile == BasementTiles.Last())
                        writer.WriteLine(tile.ExportJSON());
                    else writer.WriteLine($"{tile.ExportJSON()}, ");
                writer.WriteLine("]");
                writer.WriteLine("\"GroundTiles\": [");
                foreach (var tile in GroundTiles)
                    if (tile == GroundTiles.Last())
                        writer.WriteLine(tile.ExportJSON());
                    else writer.WriteLine($"{tile.ExportJSON()}, ");
                writer.WriteLine("]");
                writer.WriteLine("\"UpperTiles\": [");
                foreach (var tile in UpperTiles)
                    if (tile == UpperTiles.Last())
                        writer.WriteLine(tile.ExportJSON());
                    else writer.WriteLine($"{tile.ExportJSON()}, ");
                writer.WriteLine("]");
                writer.WriteLine("\"DrawableTiles\": [");
                foreach (var tile in DrawableTiles)
                    if (tile == DrawableTiles.Last())
                        writer.WriteLine(tile.ExportJSON());
                    else writer.WriteLine($"{tile.ExportJSON()}, ");
                writer.WriteLine("]");
                writer.WriteLine("\"Discard\": [");
                foreach (var tile in Discard)
                    if (tile == Discard.Last())
                        writer.WriteLine(tile.ExportJSON());
                    else writer.WriteLine($"{tile.ExportJSON()}, ");
                writer.WriteLine("]");

                // write the closing brace of the json file
                writer.WriteLine("}");
            }
        }
        public void Load(string location, string savename)
        {
            Players = new List<Player>();
            BasementTiles = new List<Tile>();
            GroundTiles = new List<Tile>();
            UpperTiles = new List<Tile>();
            DrawableTiles = new List<Tile>();
            Discard = new List<Tile>();
            using (StreamReader reader = File.OpenText(location + "\\" + savename + ".json"))
            {
                JsonData data = JsonMapper.ToObject(reader.ReadToEnd());
                CurrentPlayerIndex = int.Parse(data["CPI"].ToString());

                for (int i = 0; i < max_players; i++)
                    Players.Add(Player.ImportJSONAsObject(data["players"][i].ToJson()));

                for (int j = 0; j < data["BasementTiles"].Count; j++)
                    BasementTiles.Add(Tile.ImportJSONAsObject(data["BasementTiles"][j].ToJson()));
                for (int j = 0; j < data["GroundTiles"].Count; j++)
                    GroundTiles.Add(Tile.ImportJSONAsObject(data["GroundTiles"][j].ToJson()));
                for (int j = 0; j < data["UpperTiles"].Count; j++)
                    UpperTiles.Add(Tile.ImportJSONAsObject(data["UpperTiles"][j].ToJson()));
                for (int j = 0; j < data["DrawableTiles"].Count; j++)
                    DrawableTiles.Add(Tile.ImportJSONAsObject(data["DrawableTiles"][j].ToJson()));
                for (int j = 0; j < data["Discard"].Count; j++)
                    Discard.Add(Tile.ImportJSONAsObject(data["Discard"][j].ToJson()));
            }
        }
        public static string GetContentPack(string location, string savename)
        {
            using (StreamReader reader = File.OpenText(location + "\\" + savename + ".json"))
            {
                string json = reader.ReadToEnd();
                JsonData data = JsonMapper.ToObject(json);

                return data["pack"].ToString();
            }
        }
    }
}
