import math
from iofuncs import *
from errorfuncs import *


def calculate_max(column_stats):
    return max(column_stats)

def calculate_min(column_stats):
    return min(column_stats)

def calculate_mean(column_stats):
    return sum(column_stats) / len(column_stats)


def calculate_median(sorted_stats):
    index_middle = len(sorted_stats) // 2
    if len(sorted_stats) % 2 == 0:
        median_value = (sorted_stats[index_middle - 1] + sorted_stats[index_middle]) / 2
    else:
        median_value = sorted_stats[index_middle]
    return median_value


def calculate_percentile(values, percentile):
    values_count = len(values)
    rank = percentile / 100 * (values_count - 1)
    rank_floor = math.floor(rank)
    rank_fraction = int(rank - rank_floor)

    if rank_floor == values_count - 1:
        result = values[-1]
    else:
        result = values[rank_floor] + (values[rank_floor + 1] - values[rank_floor]) * rank_fraction
    return result


def show_percentile(sorted_stats):
    current_percentile = 0
    max_percentile = 100
    percentile_step = 5
    while current_percentile <= max_percentile:
        percentile_value = calculate_percentile(sorted_stats, current_percentile)
        output_percentiles(current_percentile, percentile_value)
        current_percentile += percentile_step


def show_stats_by_column_num(data, column_num):
    column_num = int(column_num)
    column_num -= 1
    column_keys = list(data[0].keys())
    column_value = column_keys[column_num]
    column_stats = []
    flag = False
    for row in data:
        cell_value = row[column_value].replace(' ', '')
        if cell_value == '':
            continue
        if cell_value.isalpha():
            flag = True
            break
        column_stats.append(float(cell_value))
    if flag or len(column_stats) == 0:
        column_stats.clear()
        output_column_value_error()
        return
    max_value = calculate_max(column_stats)
    min_value = calculate_min(column_stats)
    mean_value = calculate_mean(column_stats)
    sorted_stats = sorted(column_stats)
    median_value = calculate_median(sorted_stats)

    output_stats(max_value, min_value, mean_value, median_value)

    show_percentile(sorted_stats)

