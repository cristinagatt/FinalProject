def transpose_list(data):
    data_transpose = []
    for i in range(len(data[0])):
        transpose_col = []
        for row in data:
            transpose_col.append(row[i])
        data_transpose.append(transpose_col)

    return data_transpose


def show_timeline(data, date_col, encounter_type_col, encounter_type_labels, scale_col, event_col, direct_col,
                  indirect_value=1, event_labels=None, filter_col=None):
    from datetime import datetime
    from matplotlib import pyplot as plt
    from matplotlib import colors as colours
    from matplotlib import patches
    from matplotlib import cm

    date_col -= 1
    encounter_type_col -= 1
    scale_col -= 1
    event_col -= 1
    direct_col -= 1

    data_transposed = transpose_list(data)
    data_transposed[encounter_type_col] = [(int(x) if x != '' else 0) for x in data_transposed[encounter_type_col]]

    norm_map = colours.Normalize()
    norm_map.autoscale(data_transposed[encounter_type_col])

    plt.figure()

    scalar_map = cm.ScalarMappable(norm=norm_map, cmap=plt.get_cmap('gist_rainbow'))
    plt.plot([datetime.strptime(x, '%m/%d/%Y').date() for x in data_transposed[date_col]],
             [0] * len(data_transposed[0]))

    for record in data:
        if filter_col is None or record[filter_col - 1] == '1':
            text = event_labels[int(record[event_col]) - 1] if event_labels is not None else record[event_col]

            direction = -1 if int(record[direct_col]) == indirect_value else 1

            plt.annotate(text, (datetime.strptime(record[date_col], '%m/%d/%Y').date(), 0),
                         xytext=(
                             datetime.strptime(record[date_col], '%m/%d/%Y').date(),
                             direction * int(record[scale_col])),
                         arrowprops=dict(arrowstyle='-'),
                         bbox=dict(boxstyle='round', facecolor=scalar_map.to_rgba(int(record[encounter_type_col]))))

    legend = []
    for en_type in range(1, len(encounter_type_labels) + 1):
        legend.append(patches.Patch(color=scalar_map.to_rgba(en_type), label=encounter_type_labels[en_type - 1]))

    plt.legend(handles=legend, loc=9, bbox_to_anchor=(0.5, -0.05), ncol=5)
    plt.ylim(-6, 6)
    plt.show()


def get_filtered_data(data, filter_fns):
    def check_conditions(row, filter_fns):
        for i in range(len(filter_fns) - 1):
            if not filter_fns[i](row[i]):
                return False
        return True

    filtered_data = [row for row in data if check_conditions(row, filter_fns)]

    return filtered_data


if __name__ == '__main__':
    import csv
    
    encounter_type_labels = ['Internet browsing', 'Saw on TV', 'Heard on radio', 'Read on newspaper/magazine',
                             'Posted on social media myself', 'Chat with friends/family face-to-face',
                             'Overheard a conversation',
                             'Participated in the event', 'Other']
    event_labels = ['Valletta Green Festival', 'Valletta Film Festival', 'Ghanafest', 'Malta Jazz Festival',
                    'Malta International Arts Festival', 'L-Ghanja tal-Poplu - Festival', 'Darba Wahda',
                    'Il-Wards tar-Rih - Windrose', 'Malta Fashion Week', 'Blitz', 'Strada Stretta (Events)',
                    'Pageant of the Seas', 'Other']

    filename = 'c:\data 1.csv'

    ## Change values as necessary##
    date_col = 19  # Flags the 8th column as the date column (starts from 1)
    encounter_type_col = 6  # Flags the 4th column as the encounter type column
    scale_col = 7  # Flags the 5th column as the scale column
    event_col = 5  # Flags the 3th column as the event column
    direct_col = 8  # Flags the 10th column as the column for direct/indirect encounters.
	
	#date_col = (sys.argv[1])
    #encounter_type_col = (sys.argv[2])
    #scale_col = (sys.argv[3])
    #event1_col = (sys.argv[4])
    #direct_col = (sys.argv[5])

    # Keep this set to None if you're letting this script
    # filter your data, or flag the column here if you're
    # using SPSS to filter it.

    filter_col = True;

    data = list(csv.reader(open(filename)))[1:]

    # Start the function with 'lambda x:'
    # That defines that it's a function that takes a value, which we will call x
    # In this case x will represent the cell contents to be checked.
    #
    # Next put the condition to test, 'True' means don't filter, 'False' means remove all.
    # x == 5 test for a value of 5
    #
    # Description of relational operators: (that's what they're called)
    # == for equals
    # != for not equals
    # >  greater than
    # >= greater than or equals
    # <  less than
    # <= less than or equals
    # 'and' is used to combine 2 or more conditions with an AND operator (all must be true)
    # 'or' is used to combine 2 or more conditions with an OR operator (at least 1 must be true)
    # DO NOT USE THE SINGLE QUOTES I'M USING!!!!
    filter_fns = [
        lambda x: False,  # col 1
        lambda x: False,  # col 2
        lambda x: False,  # col 3
        lambda x: False,  # col 4
        lambda x: x == 7,  # col 5
        lambda x: x == 1,  # col 6
        lambda x: x == 2,  # col 7
        lambda x: x == 4,  # col 8
        lambda x: False,  # col 9
        lambda x: False,  # col 10
        lambda x: False,  # col 11
        lambda x: False,  # col 12
        lambda x: False,  # col 13
        lambda x: False,  # col 14
        lambda x: False,  # col15
        lambda x: False,  # col 16
        lambda x: False,  # col 17
        lambda x: False,  # col18
        lambda x: True,  # col 19
        lambda x: False,  # col20
        lambda x: False,  # col21
        lambda x: False,  # col22
        lambda x: False  # col23
    ];

    if (filter_col == None):
        data = get_filtered_data(data, filter_fns)

    show_timeline(data, date_col, encounter_type_col, encounter_type_labels, scale_col, event_col, direct_col,
                  indirect_value=2, event_labels=event_labels, filter_col=filter_col)


