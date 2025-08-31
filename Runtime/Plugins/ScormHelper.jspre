(function(root, factory) {
    "use strict";
    if (typeof define === 'function' && define.amd) {
        // AMD
        define([], factory);
    } else if (typeof module === 'object' && module.exports) {
        // CommonJS (NodeJS)
        module.exports = factory();
    } else {
        // Browser globals (root is window)
        root.ScormHelper = factory();
    }
}(this, function() {
    var ScormHelper = {
        ScormNotFoundError: function() {
            console.error("SCORM wrapper (pipwerks.SCORM) not found! Check script load order in index.html.");
        }
    };
	return ScormHelper;
}));
