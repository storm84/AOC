package main

import (
	"fmt"

	"github.com/storm84/AOC/AOC22/utils"
)

func main() {
	fmt.Println("test")
	arr, err := utils.ReadLines("input")
	if err != nil {
		panic(err)
	}
	resultA := 0
	resultB := 0
	for _, v := range arr {
		if v != "" {
			elf := handValue(v[0])

			youA := getYourHandValueA(v[len(v)-1])
			resultA += calculatePoints(elf, youA)

			youB := getYourHandValueB(v[0], v[len(v)-1])
			resultB += calculatePoints(elf, youB)
		}
	}
	fmt.Printf("Answer to A is: %d\n", resultA)
	fmt.Printf("Answer to B is: %d\n", resultB)
}

func getYourHandValueA(cmd byte) int {
	return handValue(cmd - 'X' + 'A')
}

func getYourHandValueB(elfCmd, yourCmd byte) int {
	var cmd byte
	switch yourCmd {
	case 'X': // lose
		cmd = 'A' + (elfCmd-'A'+2)%3
	case 'Y': // draw
		cmd = elfCmd
	case 'Z': // win
		cmd = 'A' + (elfCmd-'A'+1)%3
	}

	return handValue(cmd)
}

func handValue(b byte) int {
	switch {
	case b == 'A': // rock
		return 1
	case b == 'B': // paper
		return 2
	case b == 'C': // scissors
		return 3
	default:
		panic(fmt.Sprintf("invalid input %c", b))
	}
}

func calculatePoints(elf, you int) int {
	if elf == you {
		return 3 + you
	}

	sum := elf + you
	if sum == 4 {
		if you > elf {
			return you
		}
		return 6 + you
	} else {
		if you > elf {
			return 6 + you
		}
		return you
	}

}
