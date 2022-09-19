app.directive("limitToFloat", [function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitToFloat);
            angular.element(elem).on("keypress", function (event) {
                if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode === 190 || event.keyCode === 110 || event.keyCode === 46) {
                    if (this.value.length >= limit)
                        return false;
                }
                else
                    return false;
            });
        }
    }
}]);