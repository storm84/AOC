package utils

import (
	"os"
	"strings"
)

func ReadLines(name string) ([]string, error) {
	data, err := os.ReadFile(name)
	if err != nil {
		return nil, err
	}
	res := strings.Split(string(data), "\n")

	return res, nil
}
