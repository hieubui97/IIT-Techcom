$(document).ready(function () {

    (function($) {
        "use strict";

        jQuery.validator.addMethod('answercheck',
            function(value, element) {
                return this.optional(element) || /^\bcat\b$/.test(value);
            },
            "type the correct answer -_-");

        //validate contactForm form
        $(function() {
            $('#contactForm').validate({
                rules: {
                    Request: {
                        required: true,
                        minlength: 2
                    },
                    Name: {
                        required: true,
                        minlength: 2
                    },
                    Email: {
                        required: true,
                        email: true
                    },
                    Phone: {
                        required: true,
                        minlength: 10,
                        maxlength: 13
                    },
                    Address: {
                        required: true,
                        minlength: 2
                    }
                },
                messages: {
                    Request: {
                        required: "<p style=color:red;>Um...yea, you have to write something to send this form.</p>",
                        minlength: "<p style=color:red;>Thats all? really?</p>"
                    },
                    Name: {
                        required: "<p style=color:red;>Tên không được bỏ trống!</p>",
                        minlength: "<p style=color:red;>Tên có độ dài ít nhất 2 ký tự!</p>"
                    },
                    //subject: {
                    //    required: "come on, you have a subject, don't you?</p>",
                    //    minlength: "your subject must consist of at least 4 characters</p>"
                    //},
                    Email: {
                        required: "<p style=color:red;>Email không được bỏ trống!</p>",
                        email: "<p style=color:red;>Email không đúng định dạng: example@gmail.com!</p>"
                    },
                    Phone: {
                        required: "<p style=color:red;>Số điện thoại không được bỏ trống!</p>",
                        minlength: "<p style=color:red;>Số điện thoại có độ dài it nhất 10 số!</p>",
                        maxlength: "<p style=color:red;>Số điện thoại có độ dài tối đa 13 số!</p>"
                    },
                    Address: {
                        required: "<p style=color:red;>Địa chỉ không được bỏ trống!</p>",
                        minlength: "<p style=color:red;>Địa chỉ có độ dài it nhất 2 ký tự!</p>"
                    }

                }

                //submitHandler: function(form) {
                //    $(form).ajaxSubmit({
                //        type: "POST",
                //        data: $(form).serialize(),
                //        url: "https://localhost:44336/Home/Contact",
                //        success: function() {
                //            //$('#contactForm :input').attr('disabled', 'disabled');
                //            //$('#contactForm').fadeTo("slow",
                //            //    1,
                //            //    function() {
                //            //        $(this).find(':input').attr('disabled', 'disabled');
                //            //        $(this).find('label').css('cursor', 'default');
                //            //        $('#success').fadeIn();
                //            //        $('.modal').modal('hide');
                //            //        $('#success').modal('show');
                //            //        //window.alert("Success!");
                                    
                //            //    });
                //        },
                //        error: function() {
                //            $('#contactForm').fadeTo("slow",
                //                1,
                //                function() {
                //                    $('#error').fadeIn();
                //                    $('.modal').modal('hide');
                //                    $('#error').modal('show');
                //                });
                //        }
                //    });
                //}
            });
        });

    })(jQuery);
})