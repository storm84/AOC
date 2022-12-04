package main

import (
	"fmt"
	"strconv"
	"strings"

	"github.com/storm84/AOC/AOC22/utils"
)

type sectionRange struct {
	low  int
	high int
}

func main() {
	lines, err := utils.ReadLines("input")
	utils.Check(err)

	aCnt := 0
	bCnt := 0
	for _, line := range lines {
		if line != "" {
			pairs := strings.Split(line, ",")
			first := toSectionRange(pairs[0])
			second := toSectionRange(pairs[1])

			if isFullyOverlapping(first, second) {
				aCnt++
			}
			if isOverlapping(first, second) {
				bCnt++
			}
		}
	}
	fmt.Printf("The answer to A is: %d\n", aCnt)
	fmt.Printf("The answer to B is: %d\n", bCnt)
}

func isFullyOverlapping(a sectionRange, b sectionRange) bool {
	return a.low >= b.low && a.high <= b.high || b.low >= a.low && b.high <= a.high
}

func isOverlapping(a sectionRange, b sectionRange) bool {
	return a.low >= b.low && a.low <= b.high ||
		a.high >= b.low && a.high <= b.high ||
		b.low >= a.low && b.low <= a.high ||
		b.high >= a.low && b.high <= a.high
}

func toSectionRange(s string) sectionRange {
	split := strings.Split(s, "-")
	low, err := strconv.Atoi(split[0])
	utils.Check(err)
	high, err := strconv.Atoi(split[1])
	utils.Check(err)
	return sectionRange{low, high}
}
