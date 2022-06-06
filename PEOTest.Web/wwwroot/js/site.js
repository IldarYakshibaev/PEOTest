$(document).ready(function () {

    $("#PostId").combobox({
        funcAutocompletechange: function (event, ui, that) {

            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }

            // Search for a match (case-insensitive)
            var value = that.input.val(),
                valueLowerCase = value.toLowerCase(),
                valid = false;
            that.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });

            // Found a match, nothing to do
            if (valid) {
                return;
            }

            that.element.children("option").each(function () {
                this.selected = false;
            });
            $("#PostName").val(that.input.val());
        }
    });

    $("#toggle").on("click", function () {
        $("#PostId").toggle();
    });
});