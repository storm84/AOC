package main

import (
	"fmt"
	"sort"
	"strconv"
	"strings"

	"github.com/storm84/AOC/AOC22/utils"
)

func main() {
	lines, err := utils.ReadLines("input")
	if err != nil {
		panic(err)
	}

	var res []int
	sum := 0
	for _, l := range lines {
		if strings.TrimSpace(l) == "" {
			res = append(res, sum)
			sum = 0
		} else {
			v, err := strconv.Atoi(l)
			if err != nil {
				panic(err)
			}
			sum += v
		}
	}

	sort.Ints(res)
	fmt.Printf("The answer to A is: %d\n", res[len(res)-1])
	fmt.Printf("The answer to B is: %d\n", sumSlice(res[len(res)-3:]))
}

func sumSlice(slice []int) int {
	res := 0
	for _, v := range slice {
		res += v
	}
	return res
}
