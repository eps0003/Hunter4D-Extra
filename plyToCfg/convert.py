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
    print("A point cloud .ply file must be specified")
    exit()

if not (args[1].isdigit() and args[2].isdigit() and args[3].isdigit()):
    print("Invalid map dimensions were specified")
    exit()

width = int(args[1])
height = int(args[2])
depth = int(args[3])


def pos_to_index(x, y, z):
    return x + (z * width) + (y * width * depth)


def rgb_to_index(r, g, b):
    return (r << 17) | (g << 9) | (b << 1) | 1


with file.open() as data:
    lines = data.read().splitlines()

    start = lines.index("end_header") + 1
    lines = lines[start:]

    blocks = []

    for line in lines:
        values = [int(value) for value in line.split(" ")]
        index = pos_to_index(values[0], values[2] - int(height / 2), values[1])
        color = rgb_to_index(values[3], values[4], values[5])
        blocks.append(Block(index, color))

    blocks = sorted(blocks, key=lambda block: block[0])

    lastIndex = -1
    data = "blocks = 0;"

    for block in blocks:
        index = block[0]
        color = block[1]

        air = index - lastIndex - 1
        if air > 0:
            data += f"0;{air - 1};"

        data += f"{color};"
        lastIndex = index

    data += f"\nsize = {width};{height};{depth};"

    with open(file.stem + ".cfg", "w") as file:
        file.write(data + "\n")
