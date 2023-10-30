import csv


def get_data_from_table(csvfile):
    temp_reader = csv.DictReader(csvfile)
    return list(temp_reader)


def check_regions(data):
    result = 0
    if len(data) != 0:
        headers_line = data[0].keys()
        if 'region' in headers_line:
            result = 1
    return result


def get_regions(data):
    return sorted(set(value['region'] for value in data))


def calculate_max_column_widths(data):
    column_widths = {key: len(key) for key in data[0].keys()}
    for row in data:
        for key, value in row.items():
            column_widths[key] = max(column_widths[key], len(str(value)))
    return column_widths


def filter_data_by_region(data, region):
    return [row for row in data if row['region'] == region]


def format_row(row, width):
    return row.rjust(width)


def format_table(data):
    column_widths = calculate_max_column_widths(data)

    headers_line = data[0].keys()
    for key in headers_line:
        print(format_row(key, column_widths[key]), end='  ')
    print()

    for row in data:
        for key in row.keys():
            print(format_row(row[key], column_widths[key]), end='  ')
        print()
