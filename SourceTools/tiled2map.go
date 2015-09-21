package main

import (
	//"bytes"
	//"encoding/json"
	"encoding/xml"
	"flag"
	"fmt"
	"os"
	"path/filepath"
	"strconv"
	"strings"
)

type XMLTiledMap struct {
	XMLName     xml.Name     `xml:"map"`
	RenderOrder string       `xml:"renderorder,attr"`
	Width       int          `xml:"width,attr"`
	Height      int          `xml:"height,attr"`
	TileWidth   int          `xml:"tilewidth,attr"`
	TileHeight  int          `xml:"tileheight,attr"`
	Tilesets    []XMLTileset `xml:"tileset"`
	Layers      []XMLLayer   `xml:"layer"`
}

type XMLTileset struct {
	Name       string   `xml:"name,attr"`
	TileWidth  int      `xml:"tilewidth,attr"`
	TileHeight int      `xml:"tileheight,attr"`
	TileCount  int      `xml:"tilecount,attr"`
	FirstGID   int      `xml:"firstgid,attr"`
	Image      XMLImage `xml:"image"`
}

type XMLImage struct {
	Source string `xml:"source,attr"`
	Width  int    `xml:"width,attr"`
	Height int    `xml:"height,attr"`
}

type XMLLayer struct {
	Name   string       `xml:"name,attr"`
	Width  int          `xml:"width,attr"`
	Height int          `xml:"height,attr"`
	Data   XMLLayerData `xml:"data"`
}

type XMLLayerData struct {
	Tiles []XMLTile `xml:"tile"`
}

type XMLTile struct {
	GID int `xml:"gid,attr"`
}

func main() {
	flag.Usage = func() {
		fmt.Fprintf(os.Stderr, "Usage: %s <input.tmx> [output.hx]\n"+
			"Input must be a XML formatted Tiled project.\n"+
			"If the output file argument is missing, output will be sent to stdout\n", os.Args[0])
		flag.PrintDefaults()
	}

	flag.Parse()

	var dest *os.File

	switch flag.NArg() {
	case 0:
		// Missing required arguments
		flag.Usage()
		os.Exit(2)
	case 1:
		dest = os.Stdout
	default:
		dest, _ = os.Create(flag.Arg(1))
	}

	srcfile := flag.Arg(0)

	source, _ := os.Open(srcfile)

	var xmlmap XMLTiledMap
	xml.NewDecoder(source).Decode(&xmlmap)

	/*
		// Test if decoding works
		jsondata, _ := json.Marshal(xmlmap)
		var out bytes.Buffer
		json.Indent(&out, jsondata, "", "    ")
		out.WriteTo(dest)
	*/

	name := strings.SplitN(filepath.Base(srcfile), ".", 2)

	fmt.Fprintln(dest, generateClass(name[0], xmlmap))
}

func generateClass(name string, xmlmap XMLTiledMap) string {
	constructor := fmt.Sprintf(
		"\t\tsuper(\"%s\", %d, %d, %d, %d);\n",
		name,
		xmlmap.Width,
		xmlmap.Height,
		xmlmap.TileWidth,
		xmlmap.TileHeight)

	tilesets := make([]string, len(xmlmap.Tilesets))
	for i, tileset := range xmlmap.Tilesets {
		tilesets[i] = fmt.Sprintf(
			"\t\ttilesets.push(new Tileset(\"%s\", Assets.getBitmapData(\"%s\"), %d, %d, %d));",
			tileset.Name,
			fixPath(tileset.Image.Source),
			tileset.TileWidth,
			tileset.TileHeight,
			tileset.FirstGID)
	}

	layers := make([]string, len(xmlmap.Layers))
	for i, layer := range xmlmap.Layers {
		tileIds := make([]string, len(layer.Data.Tiles))
		for tid, tile := range layer.Data.Tiles {
			tileIds[tid] = strconv.Itoa(tile.GID)
		}
		layers[i] = fmt.Sprintf(
			"\t\tlayers.push(new MapLayer([%s], %d, %d, %d, %d, 1));",
			strings.Join(tileIds, ","),
			layer.Width,
			layer.Height,
			xmlmap.TileWidth,
			xmlmap.TileHeight)
	}

	return "package assets.map;\n\n" +
		"import openfl.Assets;\n" +
		"import graphics.Tilemap;\n\n" +
		"class " + name + " extends Tilemap {\n" +
		"\tpublic function new() {\n" +
		constructor +
		strings.Join(tilesets, "\n") + "\n" +
		strings.Join(layers, "\n") + "\n" +
		"\t}\n}\n"
}

func fixPath(src string) string {
	// Current fix: .. => assets
	return strings.Replace(src, "../", "assets/", 1)
}
