package utils

import "errors"

type Stack[T any] struct {
	data []T
}

const emptyErrorText = "Stack is empty"

func (s *Stack[T]) Push(value T) {
	s.data = append(s.data, value)
}

func (s *Stack[T]) Pop() (T, error) {
	var res T
	if len(s.data) > 0 {
		last := len(s.data) - 1
		res = s.data[last]
		s.data = s.data[:last]
		return res, nil
	}

	return res, errors.New(emptyErrorText)
}

func (s *Stack[T]) Peek() (T, error) {
	var res T
	if len(s.data) > 0 {
		last := len(s.data) - 1
		res = s.data[last]
		return res, nil
	}

	return res, errors.New(emptyErrorText)
}

func (s *Stack[T]) IsEmpty() bool {
	return len(s.data) > 0
}

func (s *Stack[T]) Size() int {
	return len(s.data)
}
