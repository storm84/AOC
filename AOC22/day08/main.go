package main

import (
	"fmt"

	"github.com/storm84/AOC/AOC22/utils"
)

func main() {
	lines, err := utils.ReadLines("input")
	utils.Check(err)

	fmt.Printf("The answer to A is: %d\n", solveA(lines))
	fmt.Printf("The answer to B is: %d\n", solveB(lines))

}
func solveA(lines []string) int {
	grid := lines[:len(lines)-1] // remove empty
	res := (len(grid) + len(grid[0]) - 2) * 2
	for i := 1; i < len(grid[0])-1; i++ {
		for j := 1; j < len(grid)-1; j++ {
			cur := grid[j][i]
			if check(cur, grid, i, j, -1, 0) || check(cur, grid, i, j, 1, 0) ||
				check(cur, grid, i, j, 0, -1) || check(cur, grid, i, j, 0, 1) {
				res++
			}
		}
	}
	return res
}

func check(value byte, grid []string, curX, curY, dirX, dirY int) bool {
	if curX+dirX < 0 || curX+dirX > len(grid[0])-1 || curY+dirY < 0 || curY+dirY > len(grid)-1 {
		return true
	}
	if value > grid[curY+dirY][curX+dirX] {
		return check(value, grid, curX+dirX, curY+dirY, dirX, dirY)
	}
	return false
}

func solveB(lines []string) int {
	grid := lines[:len(lines)-1] // remove empty
	res := 0
	for i := 1; i < len(grid[0])-1; i++ {
		for j := 1; j < len(grid)-1; j++ {
			cur := grid[j][i]
			scenicScore :=
				countTrees(cur, grid, i, j, -1, 0) * countTrees(cur, grid, i, j, 1, 0) *
					countTrees(cur, grid, i, j, 0, -1) * countTrees(cur, grid, i, j, 0, 1)
			if scenicScore > res {
				res = scenicScore
			}
		}
	}
	return res
}

func countTrees(value byte, grid []string, curX, curY, dirX, dirY int) int {
	if curX+dirX < 0 || curX+dirX > len(grid[0])-1 || curY+dirY < 0 || curY+dirY > len(grid)-1 {
		return 0
	}
	if value > grid[curY+dirY][curX+dirX] {
		return 1 + countTrees(value, grid, curX+dirX, curY+dirY, dirX, dirY)
	}
	return 1
}
