app.directive("limitToPhNo", [function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitToPhNo);
            angular.element(elem).on("keypress", function (event) {
                if (event.keyCode > 47 && event.keyCode < 58 || event.keyCode === 40 || event.keyCode === 41 || event.keyCode === 45 || event.keyCode === 32 || event.keyCode === 120 || event.keyCode === 88) {
                    if (this.value.length >= limit)
                        return false;
                }
                else
                    return false;
            });
        }
    }
}]);