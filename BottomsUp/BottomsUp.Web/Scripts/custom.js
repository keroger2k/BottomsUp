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

    var BottomsUp = BottomsUp || {};

    BottomsUp.enableAddTask = function () {
        var $btn = $('#add-new-task');
        $btn.removeAttr('disabled');
    };

    BottomsUp.addNewTaskTemplate = function () {
        var $list = $('#requirement-task-list');
        var template = $('#mustache-template').html();
        var r = Mustache.render(template, {
            rows: { requirementId : 1 }
        });
        $list.append(r);
    };


    /*
     * Must send a task object
     * Description, Number, Volume, Percentage, Comments
     *
     */
    BottomsUp.saveNewTask = function (requirementId, task, callback) {
        callback($.post('/Proposals/PutNewRequirementTask',{ 
            requirementId: requirementId, 
            task: task
        }))
    };




    $('#add-new-task').on('click', function () {
        BottomsUp.addNewTaskTemplate();
    });

    $("#requirement-task-list").on('click', ".glyphicon.glyphicon-remove", function () {

        var $self = $(this);
        var $parent = $self.parents('li');
        var id = $parent.data('id');

        $parent.hide('250', function () {
            $parent.remove();
        });


    });

    $("#requirement-task-list").on("submit", "form", function (event) {
        event.preventDefault();
        $.post('/Proposals/PutNewRequirementTask', $(this).serialize())
    });


    //$("#requirement-task-list").on('click', ".glyphicon.glyphicon-floppy-save", function () {

    //    var $self = $(this);
    //    var form = $self.parents('form');

    //    form.validate({
    //        submitHandler: function (form) {
    //            form.ajaxSubmit();
    //        }
    //    });



    //    var $parent = $self.parents('li');
    //    var requirementId = $parent.parent().data('id');
    //    var task = {
    //        Id: $parent.data('id'),
    //        Description: $parent.find('input[name="description"]').val(),
    //        Number: $parent.find('input[name = "number"]').val(),
    //        Volume: $parent.find('input[name = "volume"]').val(),
    //        Percentage: $parent.find('input[name = "percentage"]').val(),
    //        Comments: $parent.find('input[name = "comments"]').val()
    //    };
    //    //if (task.Id === '') {
    //    //    //new task
    //    //    BottomsUp.saveNewTask(requirementId, task, function (xhr) {
    //    //        xhr.success(function (data) {
    //    //            console.log(data);
    //    //            $parent.data('id', data.task.Id);
    //    //        });
    //    //    });
    //    //} else {
    //    //    //existing task
    //    //    console.log('existing');

    //    //}
    //});




    var mySlidebars = new $.slidebars();
    $('.my-button').on('click', function () {
        mySlidebars.slidebars.toggle('left');
    });

    $(".pws-task-item").on('click', function () {

        var $self = $(this);

        $.getJSON('/Proposals/GetRequirementTasks', { id: $self.data('id') }, function (data) {
            var $list = $('#requirement-task-list');
            $list.empty();
            var template = $('#mustache-template').html();
            var r = Mustache.render(template, {
                rows: data
            });
            $list.append(r);
            $('.dropdown-toggle').dropdown();
            $list.data('id', $self.data('id'));
            BottomsUp.enableAddTask();
        });
    });

    $(".pws-task-labor-breakdown").on('click', function (e) {
        e.stopPropagation();
    });

    $('.pws-lc-item span.glyphicon-remove').on('click', function (e) {
        var $self = $(this);
        var parentItem = $self.parents('li.pws-task-labor-item');

        $.getJSON('/Proposals/DeleteRequirementTasks', { id: $self.data('id') }, function (data) {
            var $list = $('#requirement-task-list');
            $list.empty();
            var template = $('#mustache-template').html();
            var r = Mustache.render(template, {
                rows: data
            });
            $list.append(r);
            $list.data('id', $self.data('id'));
            BottomsUp.enableAddTask();
        });





        parentItem.hide('250', function () {
            parentItem.remove();
        });
    });

})(jQuery);