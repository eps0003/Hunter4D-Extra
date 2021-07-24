import sys
from collections import namedtuple
from pathlib import Path

Block = namedtuple("Block", ["index", "color"])

args = sys.argv[1:]
if len(args) < 1:
    print("No file was specified")
    exit()

if len(args) < 4:
    print("No map dimensions were specified")
    exit()

file = Path(args[0])
if not file.is_file():
    print("Invalid file was specified")
    exit()

if file.suffix != ".ply":
    print("A .ply file must be specified")
    exit()

if not (args[1].isdigit() and args[2].isdigit() and args[3].isdigit()):
    print("Invalid map dimensions were specified")
    exit()

width = int(args[1])
height = int(args[2])
depth = int(args[3])


def posToIndex(x, y, z):
    return x + (z * width) + (y * width * depth)


def rgbToIndex(r, g, b):
    return (255 << 24) | (r << 16) | (g << 8) | b


with file.open() as data:
    lines = data.read().splitlines()

    start = lines.index("end_header") + 1
    lines = lines[start:]

    blocks = []

    for line in lines:
        values = [int(value) for value in line.split(" ")]
        index = posToIndex(values[0], values[2] - int(height / 2), values[1])
        color = rgbToIndex(values[3], values[4], values[5])
        blocks.append(Block(index, color))

    blocks = sorted(blocks, key=lambda block: block[0])

    lastIndex = -1
    encoded = "blocks = "

    for block in blocks:
        index = block[0]
        color = block[1]

        air = index - lastIndex - 1
        if air > 0:
            encoded += f"0;{air - 1};"
            pass

        encoded += f"{color};"

        lastIndex = index

    encoded += f"\nsize = {width};{height};{depth};"

    with open(file.stem + ".cfg", "w") as file:
        file.write(encoded + "\n")
