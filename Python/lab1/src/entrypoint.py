from tablefuncs import *
from statsfuncs import *
from errorfuncs import *


def start():
    path = input_file_path()
    file_check_result = check_file_path(path)

    if check_file_path(path):
        show_error_message(file_check_result)
        return

    with open(path, mode='r') as csvfile:
        data = get_data_from_table(csvfile)

    if not check_regions(data):
        show_error_message(NO_REGION_ERROR)
        return

    all_regions = get_regions(data)
    output_regions(all_regions)

    region = input_region()

    filtered_data = filter_data_by_region(data, region)

    if not filtered_data:
        show_error_message(REGION_ERROR)
        return

    format_table(filtered_data)

    column_counter = len(filtered_data[0])
    output_number_of_columns(column_counter)

    column_num = input_column_num()
    if not check_column_num(column_num, column_counter):
        show_error_message(COLUMN_ERROR)
        return

    show_stats_by_column_num(filtered_data, column_num)
