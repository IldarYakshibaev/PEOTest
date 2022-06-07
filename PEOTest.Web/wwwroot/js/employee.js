$(document).ready(function () {
    //$.validator.setDefaults({ ignore: [] });

    CompanyCB();
    SubdivisionCB();
    PostCB();
});

CompanyCB = function () {
    $('#CompanyId').combobox({
        funcAutocompletechange: function (event, ui, that) {

            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }

            // Search for a match (case-insensitive)
            var value = that.input.val(),
                valueLowerCase = value.toLowerCase(),
                valid = false;
            that.element.children('option').each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase && $(this).text().toLowerCase() !== "") {
                    this.selected = valid = true;
                    return false;
                }
            });

            // Found a match, nothing to do
            if (valid) {
                return;
            }

            that.element.children('option').each(function () {
                this.selected = false;
            });
            $('#CompanyName').val(that.input.val());
        },
        inputName: '#CompanyName'
    });
}

SubdivisionCB = function () {
    $('#SubdivisionId').combobox({
        funcAutocompletechange: function (event, ui, that) {

            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }

            // Search for a match (case-insensitive)
            var value = that.input.val(),
                valueLowerCase = value.toLowerCase(),
                valid = false;
            that.element.children('option').each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase && $(this).text().toLowerCase() !== "") {
                    this.selected = valid = true;
                    return false;
                }
            });

            // Found a match, nothing to do
            if (valid) {
                return;
            }

            that.element.children('option').each(function () {
                this.selected = false;
            });
            $('#SubdivisionName').val(that.input.val());
        },
        inputName: '#SubdivisionName'
    });
}

PostCB = function () {
    $('#PostId').combobox({
        funcAutocompletechange: function (event, ui, that) {

            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }

            // Search for a match (case-insensitive)
            var value = that.input.val(),
                valueLowerCase = value.toLowerCase(),
                valid = false;
            that.element.children('option').each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase && $(this).text().toLowerCase() !== "") {
                    this.selected = valid = true;
                    return false;
                }
            });

            // Found a match, nothing to do
            if (valid) {
                return;
            }

            that.element.children('option').each(function () {
                this.selected = false;
            });
            $('#PostName').val(that.input.val());
        },
        inputName: '#PostName'
    });
}



$.validator.setDefaults({ ignore: [":hidden:not(#CompanyName)", ":hidden:not(#SubdivisonName)", ":hidden:not(#PostName)"] });