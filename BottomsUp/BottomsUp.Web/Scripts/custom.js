;(function ($) {

    var mySlidebars = new $.slidebars();
    $('.my-button').on('click', function () {
        mySlidebars.slidebars.toggle('left');
    });

    $(".pws-task-item").on('click', function () {

        var $self = $(this);
        var $taskList = $self.find(".pws-task-labor-breakdown");

        if ($taskList.is(":hidden")) {
            $taskList.slideDown(250);
        } else {
            $taskList.hide(250);
        }
    });

    $(".pws-task-labor-breakdown").on('click', function (e) {
        e.stopPropagation();
    });


})(jQuery);