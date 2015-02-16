$.validator.setDefaults({
    errorElement: "span",
    errorClass: "help-block",
    highlight: function (element, errorClass, validClass) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length || element.prop('type') === 'checkbox' || element.prop('type') === 'radio') {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    }
});

; (function ($) {




    var mySlidebars = new $.slidebars();
    $('.my-button').on('click', function () {
        mySlidebars.slidebars.toggle('left');
    });

    $(".pws-task-item").on('click', function () {

        var $self = $(this);

        $.getJSON('/Proposals/GetRequirementTasks', { id: $self.data('id') }, function (data) {
            var $table = $('#task-table tbody');
            console.log(data);

            $table.empty();


            var template = $('#mustache-template').html();
            console.log(r);
            var r = Mustache.render(template, {
                rows: data
            });
            $table.append(r);
        });
    });

    $(".pws-task-labor-breakdown").on('click', function (e) {
        e.stopPropagation();
    });

    $('.pws-lc-item span.glyphicon-minus').on('click', function (e) {
        var $self = $(this);
        var parentItem = $self.parents('li.pws-task-labor-item');
        parentItem.hide('250', function () {
            parentItem.remove();
        });
    });

    $('.pws-lc-item span.glyphicon-plus').on('click', function (e) {
        var $self = $(this);
        var form = $self.parents('form');

        form.validate({
            submitHandler: function (form) {
                form.ajaxSubmit();
            }
        });

    });

})(jQuery);