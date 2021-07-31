import sys
from pathlib import Path

import numpy
from PIL import Image

args = sys.argv[1:]
if len(args) < 1:
    print("No file was specified")
    exit()

file = Path(args[0])
if not file.is_file():
    print("Invalid file was specified")
    exit()

if file.suffix != ".vxl":
    print("An Ace of Spades map file (.vxl) must be specified")
    exit()

width = 512
height = 64
depth = 512

byteArr = bytearray()
map = numpy.zeros((width, height, depth))
color = numpy.zeros((width, height, depth))


def pos_to_index(x, y, z):
    return x + (z * width) + (y * width * depth)


def index_to_pos(index):
    return (
        index % width,
        int(index / (width * depth)),
        int(index / width) % depth
    )


def set_geom(x, y, z, t):
    assert y >= 0 and y < height
    map[x][y][z] = t


def set_color(x, y, z, c):
    assert y >= 0 and y < height
    color[x][y][z] = c


def color_rgb(r, g, b):
    return (r << 17) | (g << 9) | (b << 1) | 1


def load_map():
    v = 0
    for z in range(depth):
        for x in range(width):
            for y in range(height):
                set_geom(x, y, z, 1)
                set_color(x, y, z, color_rgb(100, 100, 100))
            while True:
                number_4byte_chunks = byteArr[v]
                top_color_start = byteArr[v + 1]
                top_color_end = byteArr[v + 2]

                for i in range(top_color_start):
                    set_geom(x, i, z, 0)

                color_index = v + 4
                for y in range(top_color_start, top_color_end + 1):
                    set_color(x, y, z, color_rgb(byteArr[color_index + 2], byteArr[color_index + 1], byteArr[color_index]))
                    color_index += 4

                len_bottom = top_color_end - (top_color_start + 1)

                # check for end of data marker
                if number_4byte_chunks == 0:
                    # infer ACTUAL number of 4-byte chunks from the length of the color data
                    v += 4 * (len_bottom + 1)
                    break

                # infer the number of bottom colors in next span from chunk length
                len_top = (number_4byte_chunks - 1) - len_bottom

                # now skip the v pointer past the data to the beginning of the next span
                v += number_4byte_chunks * 4

                bottom_color_end = byteArr[v + 3]  # aka air start
                bottom_color_start = bottom_color_end - len_top

                for y in range(bottom_color_start, bottom_color_end):
                    set_color(x, y, z, color_rgb(byteArr[color_index + 2], byteArr[color_index + 1], byteArr[color_index]))
                    color_index += 4


def encode_blocks():
    data = "blocks = 0;"

    prev_visible = True
    last_visible_index = 0
    i = 0

    for y in range(height):
        for z in range(depth):
            for x in range(width):

                visible = map[x][y][z] == 1

                if visible:
                    if not prev_visible:
                        data += f"0;{i - last_visible_index - 2}" + ";"

                    block = color[x][y][z]
                    data += f"{int(block)};"
                    last_visible_index = i

                prev_visible = visible
                i += 1

                if i % 1000000 == 0:
                    print(f"{i / 1000000}/{(width*height*depth) / 1000000}")

    return data


with file.open("rb") as data:
    byteArr = bytearray(data.read())

    print("Parsing file")
    load_map()

    img = Image.new(mode="RGB", size=(width, depth))

    for x in range(width):
        for z in range(depth):
            for y in range(height):
                if (map[x][height - y - 1][z] == 1):
                    img.putpixel((x, z), (255, 0, 0))
                    break

    img.show()

    # print("Encoding blocks")
    # data = encode_blocks()

    # data += f"\nsize = {width};{height};{depth};"

    # with open(file.stem + ".cfg", "w") as file:
    #     file.write(data + "\n")

    print("Done!")
