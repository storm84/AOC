package main

import (
	"fmt"
	"strconv"
	"strings"

	"github.com/storm84/AOC/AOC22/utils"
)

func main() {
	lines, err := utils.ReadLines("input")
	utils.Check(err)

	fmt.Printf("The anser to A is: %s\n", solve(lines, false))
	fmt.Printf("The anser to B is: %s\n", solve(lines, true))

}

func solve(lines []string, is9001 bool) string {
	stacksInitiated := false
	var stacks []utils.Stack[string]
	for i, l := range lines {
		if l == "" && !stacksInitiated {
			stacks = initStacks(lines[:i])
			stacksInitiated = true
		} else if l != "" && stacksInitiated {

			move(stacks, lines[i], is9001)
		}
	}
	return getHeadString(stacks)
}

func getHeadString(stacks []utils.Stack[string]) string {
	var res string
	for _, stack := range stacks {
		v, _ := stack.Peek()
		res += v
	}
	return res
}

func move(stacks []utils.Stack[string], cmd string, is9001 bool) []utils.Stack[string] {
	cmdFields := strings.Fields(cmd)
	n, _ := strconv.Atoi(cmdFields[1])
	from, _ := strconv.Atoi(cmdFields[3])
	to, _ := strconv.Atoi(cmdFields[5])

	for i := 0; i < n; i++ {
		val, err := stacks[from-1].Pop()
		utils.Check(err)
		if is9001 {
			defer stacks[to-1].Push(val)
		} else {
			stacks[to-1].Push(val)
		}

	}
	return stacks
}

func initStacks(lines []string) []utils.Stack[string] {
	rows := strings.Fields(lines[len(lines)-1])
	stacks := make([]utils.Stack[string], len(rows))
	for i := len(lines) - 2; i >= 0; i-- {
		for j := 1; j < len(lines[i])-1; j += 4 {
			val := lines[i][j : j+1]
			if val != " " {
				stacks[j/4].Push(val)
			}
		}
	}
	return stacks
}
