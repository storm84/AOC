package main

import (
	"fmt"
	"strconv"
	"strings"

	"github.com/storm84/AOC/AOC22/utils"
)

type node struct {
	name     string
	size     int
	files    []file
	parent   *node
	children []*node
}

type file struct {
	name string
	size int
}

func main() {
	lines, err := utils.ReadLines("input")
	utils.Check(err)

	if lines[0] == "$ cd /" {
		ptr := 0
		root := buildTree(&node{name: "/"}, lines, &ptr)
		fmt.Printf("The answer to A is: %d\n", solveA(root))
		fmt.Printf("The answer to B is: %d\n", solveB(root))

	}
}
func solveB(n *node) int {
	const total = 70000000
	const required = 30000000
	toFree := required - (total - n.size)

	res := 0
	diff := n.size - toFree
	var req func(nreq *node)
	req = func(nreq *node) {
		for _, c := range nreq.children {
			req(c)
		}
		if nreq.size >= toFree && nreq.size-toFree < diff {
			res += nreq.size
			diff = nreq.size - toFree
		}
	}
	req(n)
	return res
}
func solveA(n *node) int {
	res := 0
	var req func(nreq *node)
	req = func(nreq *node) {
		for _, c := range nreq.children {
			req(c)
		}
		if nreq.size <= 100000 {
			res += nreq.size
		}
	}
	req(n)

	return res

}

func buildTree(n *node, lines []string, next *int) *node {
	if *next > len(lines) || lines[*next] == "" {
		return n
	}

	for *next < len(lines) {
		cmds := strings.Split(lines[*next], " ")

		switch cmds[0] {
		case "$":
			switch cmds[1] {
			case "cd":
				switch cmds[2] {
				case "..":
					return n
				default:
					*next += 1
					n.children = append(n.children, buildTree(&node{name: cmds[2], parent: n}, lines, next))
				}
			case "ls":
				//do nothing
			}
		case "dir":
			//do nothing
		case "":
			return n
		default:
			i, _ := strconv.Atoi(cmds[0])
			n.files = append(n.files, file{name: cmds[1], size: i})
			calcDirSize(n, i)
		}
		*next += 1
	}
	return n
}

func calcDirSize(n *node, fileSize int) {
	n.size += fileSize
	if n.parent != nil {
		calcDirSize(n.parent, fileSize)
	}
}
