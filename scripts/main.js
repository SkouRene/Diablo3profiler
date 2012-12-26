/// <reference path="jquery-1.8.2.intellisense.js" />

//Remember to remove intellisence after development !!

//document ready function start.
$(document).ready(function () {

    $('#btnGetMoreHeroes').click(function () {
        
        $('#viewHeroes').fadeOut('slow', function () {
            $('#getHeroes').fadeIn('slow');
        });
    });
   

});

//.net pageload event
function pageLoad() {

   

}



/* AJAX METHODS PART !!
*
*/


// Add code which needs to be executed after async call
jQuery.fn.extend({
    AjaxReady: function (fn) {
        if (fn) {
            return jQuery.event.add(this[0], "AjaxReady", fn, null);
        } else {
            var ret = jQuery.event.trigger("AjaxReady", null, this[0], false, null);
            // if there was no return value then the even validated correctly  
            if (ret === undefined)
                ret = true;
            return ret;
        }
    }
});

$(document).AjaxReady(function () {

    
});

// End ajax ready function

//functions


