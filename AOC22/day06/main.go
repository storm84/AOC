package main

import (
	"fmt"

	"github.com/storm84/AOC/AOC22/utils"
)

func main() {
	lines, err := utils.ReadLines("input")
	utils.Check(err)

	data := lines[0]
	fmt.Printf("The answer to A is %d\n", solve(data, 4))
	fmt.Printf("The answer to B is %d\n", solve(data, 14))
}

func solve(data string, subStringSize int) int {
	for i := subStringSize; i < len(data); i++ {
		if isUnique(data[i-subStringSize : i]) {
			return i
		}
	}
	return -1
}

func isUnique(input string) bool {
	m := make(map[rune]bool, len(input))
	for _, r := range input {
		_, ok := m[r]
		if ok {
			return false
		}
		m[r] = true
	}
	return true
}
