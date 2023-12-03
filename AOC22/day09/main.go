package main

import (
	"fmt"
	"strconv"
	"strings"

	"github.com/storm84/AOC/AOC22/utils"
)

type position struct {
	x int
	y int
}
type rope struct {
	//head position
	//tail position
	knots []position
}
type direction position

func main() {
	lines, err := utils.ReadLines("input")
	utils.Check(err)

	fmt.Printf("The answer to A is: %d\n", solve(lines, 2))
	fmt.Printf("The answer to B is: %d\n", solve(lines, 10))
}

func solve(lines []string, noOfKnots int) int {
	//r := rope{position{0, 0}, position{0, 0}}
	//r := rope{[]position{{0, 0}, {0, 0}}}
	r := rope{}
	for i := 0; i < noOfKnots; i++ {
		r.knots = append(r.knots, position{})

	}

	m := make(map[position]bool)

	for _, line := range lines {
		if line != "" {
			cmds := strings.Split(line, " ")
			dir := getDirection(cmds[0])
			n, err := strconv.Atoi(cmds[1])
			utils.Check(err)
			for i := 0; i < n; i++ {
				r.move(dir)
				_, ok := m[r.knots[len(r.knots)-1]]
				if !ok {
					m[r.knots[len(r.knots)-1]] = true
				}
			}
		}
	}

	return len(m)
}

//fmt.Printf("%s head: {x:%d, y:%d}, tail: {x:%d, y:%d}\n", line, r.head.x, r.tail.y, r.tail.x, r.tail.y)

// func (r *rope) moveHead(dir direction) {
// 	r.head.x += dir.x
// 	r.head.y += dir.y

// 	if r.head.x-r.tail.x > 1 {
// 		r.tail.x++
// 		r.tail.y = r.head.y
// 	} else if r.head.x-r.tail.x < -1 {
// 		r.tail.x--
// 		r.tail.y = r.head.y
// 	}

// 	if r.head.y-r.tail.y > 1 {
// 		r.tail.y++
// 		r.tail.x = r.head.x

// 	} else if r.head.y-r.tail.y < -1 {
// 		r.tail.y--
// 		r.tail.x = r.head.x

// 	}

// }
func (r *rope) move(dir direction) {
	r.knots[0].x += dir.x
	r.knots[0].y += dir.y

	for i := 1; i < len(r.knots); i++ {
		xDiff := r.knots[i-1].x - r.knots[i].x
		yDiff := r.knots[i-1].y - r.knots[i].y

		if xDiff > 1 {
			r.knots[i].x++
			if r.knots[i].y < r.knots[i-1].y {
				r.knots[i].y++
			} else if r.knots[i].y > r.knots[i-1].y {
				r.knots[i].y--
			}
		} else if xDiff < -1 {
			r.knots[i].x--
			if r.knots[i].y < r.knots[i-1].y {
				r.knots[i].y++
			} else if r.knots[i].y > r.knots[i-1].y {
				r.knots[i].y--
			}
		}

		if yDiff > 1 {
			r.knots[i].y++
			if r.knots[i].x < r.knots[i-1].x {
				r.knots[i].x++
			} else if r.knots[i].x > r.knots[i-1].x {
				r.knots[i].x--
			}

		} else if yDiff < -1 {
			r.knots[i].y--
			if r.knots[i].x < r.knots[i-1].x {
				r.knots[i].x++
			} else if r.knots[i].x > r.knots[i-1].x {
				r.knots[i].x--
			}
		}

	}

}

func getDirection(dirStr string) direction {
	switch dirStr {
	case "U":
		return direction{0, 1}
	case "D":
		return direction{0, -1}
	case "R":
		return direction{1, 0}
	case "L":
		return direction{-1, 0}
	default:
		panic("Invalid input")
	}
}
