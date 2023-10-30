import os
from errorfuncs import *


def input_file_path():
    return input('Enter path file:\n')


def check_file_path(path):
    if not path.endswith('.csv'):
        return FILE_EXTENSION_ERROR
    elif not os.path.exists(path):
        return FILE_NOT_FOUND_ERROR
    elif os.stat(path).st_size == 0:
        return EMPTY_FILE_ERROR
    else:
        return 0


def output_regions(regions_set):
    print('\nList of regions:\n')
    for value in regions_set:
        print(value)


def input_region():
    return input('\nEnter region to filter:\n')


def output_number_of_columns(column_counter):
    print(f'\nThere is/are {column_counter} column(s) in table.\n')


def input_column_num():
    return input('Enter column number:\n')


def check_column_num(column_num, column_counter):
    result = 0
    if column_num.isdigit() and '.' not in column_num:
        column_id = int(column_num)
        if 0 < column_id <= column_counter:
            result = column_id
    return result


def output_stats(max_value, min_value, mean_value, median_value):
    print(f'Max: {max_value}')
    print(f'Min: {min_value}')
    print(f'Mean: {round(mean_value, 2)}')
    print(f'Median: {round(median_value, 2)}\n')


def output_percentiles(percentile_number, percentile_value):
    print(f'{str(percentile_number).rjust(3)}% {round(percentile_value, 2)}')
