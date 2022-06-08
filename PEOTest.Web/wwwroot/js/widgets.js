$.widget("custom.combobox", {
    options: {
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

            // Remove invalid value
            that.input
                .val("")
                .attr("title", value + " didn't match any item")
                .tooltip("open");
            that.element.val("");
            that._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            that.input.autocomplete("instance").term = "";
        },
        inputName: ""
    },
    _create: function () {
        this.wrapper = $("<span>")
            .addClass("custom-combobox")
            .insertAfter(this.element);

        this.element.hide();
        this._createAutocomplete();
        this._createShowAllButton();
    },

    _createAutocomplete: function () {
        var selected = this.element.children(":selected"),
            value = selected.val() ? selected.text() : "";

        this.input = $("<input>")
            .appendTo(this.wrapper)
            .val(value)
            .attr("title", "")
            .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
            .autocomplete({
                delay: 0,
                minLength: 0,
                source: this._source.bind(this)
            })
            .tooltip({
                classes: {
                    "ui-tooltip": "ui-state-highlight"
                }
            });

        if (this.options.inputName !== "") {
            $(this.options.inputName).val(value);
        }

        this._on(this.input, {
            autocompleteselect: function (event, ui) {
                ui.item.option.selected = true;
                this._trigger("select", event, {
                    item: ui.item.option
                });

                if (this.options.inputName !== "") {
                    $(this.options.inputName).val(ui.item.value);
                }
            },

            autocompletechange: "_removeIfInvalid"
        });
    },

    _createShowAllButton: function () {
        var input = this.input,
            wasOpen = false;

        $("<a>")
            .attr("tabIndex", -1)
            .attr("title", "Показать все элементы")
            .tooltip()
            .appendTo(this.wrapper)
            .button({
                icons: {
                    primary: "ui-icon-triangle-1-s"
                },
                text: false
            })
            .removeClass("ui-corner-all")
            .addClass("custom-combobox-toggle ui-corner-right")
            .on("mousedown", function () {
                wasOpen = input.autocomplete("widget").is(":visible");
            })
            .on("click", function () {
                input.trigger("focus");

                // Close if already visible
                if (wasOpen) {
                    return;
                }

                // Pass empty string as value to search for, displaying all results
                input.autocomplete("search", "");
            });
    },

    _source: function (request, response) {
        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
        response(this.element.children("option").map(function () {
            var text = $(this).text();
            if (this.value && (!request.term || matcher.test(text)))
                return {
                    label: text,
                    value: text,
                    option: this
                };
        }));
    },

    _removeIfInvalid: function (event, ui) {
        this.options.funcAutocompletechange(event, ui, this);
    },

    _destroy: function () {
        this.wrapper.remove();
        this.element.show();
    }
});