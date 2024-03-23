//PriceFilter
const rangeInput = document.querySelectorAll(".range-input input");
const priceValue = document.querySelectorAll(".price-value");
const range = document.querySelector(".progress");
let priceGap = 10;

function validateRange(e) {
    let minVal = parseInt(rangeInput[0].value);
    let maxVal = parseInt(rangeInput[1].value);

    if (maxVal - minVal < priceGap) {
        if (e.target.className === "range-min") {
            rangeInput[0].value = maxVal - priceGap;
        } else {
            rangeInput[1].value = minVal + priceGap;
        }
    } else {
        priceValue[0].textContent = `$${minVal}`;
        priceValue[1].textContent = `$${maxVal}`;
        range.style.left = `${(minVal / rangeInput[0].max) * 100}%`;
        range.style.right = `${100 - (maxVal / rangeInput[1].max) * 100}%`;
    }
}

rangeInput.forEach((input) => {
    input.addEventListener("input", validateRange);
});

//validateRange();

//categoryplus
var toggleIcons = document.querySelectorAll('.toggleIcon');
toggleIcons.forEach(function (icon) {
    icon.addEventListener('click', function () {
        var targetId = icon.getAttribute('data-target');
        var targetUl = document.getElementById(targetId);
        if (targetUl.style.display === 'none') {
            targetUl.style.display = 'block';
            icon.classList.remove('fa-plus');
            icon.classList.add('fa-minus');
        } else {
            targetUl.style.display = 'none';
            icon.classList.remove('fa-minus');
            icon.classList.add('fa-plus');
        }
    });
});



document.addEventListener('DOMContentLoaded', function () {

    var replyLinks = document.querySelectorAll('.comment-reply-link');
    replyLinks.forEach(function (replyLink) {
        replyLink.addEventListener('click', function (event) {
            event.preventDefault(); 
            var commentId = this.getAttribute('data-commentid');
            console.log(commentId);
            var commentFormContainer = document.getElementById('commentFormContainer-' + commentId);
            if (commentFormContainer) {
                document.querySelectorAll('.post-comments-form').forEach(function (form) {
                    if (form.id !== 'commentFormContainer-' + commentId) {
                        form.style.display = 'none';
                    }
                });
                commentFormContainer.style.display = (commentFormContainer.style.display === 'none' || commentFormContainer.style.display === '') ? 'flex' : 'none';
            }
        });
    });
});

//function showReplyForm(commentId, username) {
//    var replyFormContainer = document.getElementById("commentFormContainer-" + commentId);
//    if (replyFormContainer.style.display === "none") {

//        hideAllReplyForms();
//        // Append the reply form below the corresponding comment
//        var comment = document.getElementById("comment-" + commentId);
//        if (comment) {
//            var replyForm = document.getElementById("commentFormContainer-" + commentId);
//            comment.appendChild(replyForm);
//            replyForm.style.display = "block";
//        }
//    } else {
//        replyFormContainer.style.display = "none";
//    }
//}

//function hideAllReplyForms() {
//    var replyForms = document.querySelectorAll(".post-comments-form");
//    replyForms.forEach(function (form) {
//        form.style.display = "none";
//    });
//}


