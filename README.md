# Identicons
A generator for identicons, from arbitrary strings.

# Objectives

- The generator will produce 8x8 pixel PNG images, which should be legible at a variety of sizes.
- The generator must be deterministic for a given input string.
- In general, any two images should be readily distinct from one another, so long as their input strings are distinct in any way. (This is particularly important for similar input strings, where e.g. users would ALREADY be likely to mix each other up.)
- To the greatest degree possible, all output images should be fairly vibrant and asthetically pleasing.
- Images should not convey any specific meaning or be offensive in some way.
- There should be a variety of different images, such that they don't all look like slight variations on one another.

# Usage

The executable can be invoked with the following parameters:

```
  --string <string> (REQUIRED)  Input string used to generate an identicon. Required.
  --output <output> (REQUIRED)  Output file to write. Can omit extension (and it will be altered if given but
                                incorrect). Required.
  --version                     Show version information
  -?, -h, --help                Show help and usage information
```

# Generation Logic

The basic generation logic is as follows:
- The MD5 hash of the input string is produced (128 bits).
- The first three bits\* are used to select the the basic pattern (i.e., whether the image is mirrored along one or more axes, has rotational symmetry, etc.).
- The next two bits are used to determine a category of color scheme.
- The following three bits are used to determine the base color for the color scheme.
- The remaining bits are used, with a rolling 3-bit window, to determine the color of individual pixels as needed for the selected pattern.

\* Technically only two bits are needed to select a pattern, but the third bit is reserved in case we want to add more patterns later.

## Patterns

The following four patterns are used:

- Mirrored across the horizontal midline
- Mirrored across the vertical midline
- Mirrored across both midlines
- One-half filled in and then rotated 180 degrees

(Initially there was also an implementation to fill in one corner and rotate it 4 times, but this had an unfortunate tendency to produce swastikas, which was a pretty big violation of one of the objectives.)

## Color Schemes

The following four color schemes are used:

- Monochrome - pick a random hue and take a gradient of various shades
- Divergent - pick a random hue and use values from it to the opposite side of the color wheel
- Triadic - pick a random hue and use it along with the two values 1/3rd of the way around the color wheel
- Tetradic - pick a random hue and use it along with the three values 1/4th of the way around the color wheel'

Note that the first two produce eight different values in a gradient, whereas the last two just produce 3 and 4 colors, respectively.