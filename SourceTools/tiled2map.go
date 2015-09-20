package main

import (
	"bytes"
	"encoding/json"
	"encoding/xml"
	"flag"
	"fmt"
	"os"
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

	source, _ := os.Open(flag.Arg(0))

	var xmlmap XMLTiledMap
	xml.NewDecoder(source).Decode(&xmlmap)

	/*
		// Test if decoding works
		jsondata, _ := json.Marshal(xmlmap)
		var out bytes.Buffer
		json.Indent(&out, jsondata, "", "    ")
		out.WriteTo(dest)
	*/
}
