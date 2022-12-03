package main

import (
	"fmt"
	"strings"
	"unicode"

	"github.com/storm84/AOC/AOC22/utils"
)

func main() {
	lines, err := utils.ReadLines("input")
	if err != nil {
		panic(err)
	}

	fmt.Printf("The answer to A is: %d\n", solveA(lines))
	fmt.Printf("The answer to B is: %d\n", solveB(lines))
}

func solveA(lines []string) int {
	prioritySum := 0
	for _, line := range lines {
		mid := len(line) / 2
		first := line[:mid]
		second := line[mid:]
		for _, r := range first {
			if strings.ContainsRune(second, r) {
				prioritySum += priority(r)
				break
			}
		}
	}
	return prioritySum
}
func solveB(lines []string) int {
	badgePrioritySum := 0
	for i := 0; i < len(lines)/3; i++ {
		pos := i * 3
		for _, r := range lines[pos] {
			if strings.ContainsRune(lines[pos+1], r) &&
				strings.ContainsRune(lines[pos+2], r) {
				badgePrioritySum += priority(r)
				break
			}
		}
	}
	return badgePrioritySum
}
func priority(r rune) int {
	if unicode.IsUpper(r) {
		return int(r - 'A' + 27)
	}
	return int(r - 'a' + 1)
}
