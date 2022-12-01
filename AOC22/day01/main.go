package main

import (
	"fmt"
	"os"
	"sort"
	"strconv"
	"strings"
)

func main() {
	lines, err := readLines("input")
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

func readLines(name string) ([]string, error) {
	data, err := os.ReadFile(name)
	if err != nil {
		return nil, err
	}
	res := strings.Split(string(data), "\n")

	return res, nil
}
