<template>

    <button ref="component" type="button" :class="componentStyles" :disabled="props.disabled"
            class="overflow-hidden text-md theme-text-primary theme-action-shadow theme-action-border-round">
        <span class="inline-flex items-center w-full h-full theme-action-overlay theme-action-transition theme-action-border-round theme-action-padding"
              :class="contentStyles">
            <i v-if="icon" :class="icon"></i>
            <span class="theme-action-transition">
                {{ text }}
            </span>
        </span>
    </button>

</template>

<script setup lang="ts">

import { ButtonType } from "@/common/components/appButton/ButtonType";
import { computed, type PropType, ref } from "vue";
import { ActionComponentSize } from "@/common/constants/ActionComponentSize";
import { ButtonLayout } from "./ButtonLayout";

const props = defineProps({
    type: {
        type: Number as PropType<ButtonType>,
        default: ButtonType.Primary
    },
    text: {
        type: String,
        default: '',
    },
    disabled: {
        type: Boolean,
        default: false
    },
    icon: {
        type: String,
        default: ''
    },
    layout: {
        type: Number as PropType<ButtonLayout>,
        default: ButtonLayout.Rectangle 
    },
    size: {
        type: Number as PropType<ActionComponentSize>,
        default: ActionComponentSize.Medium
    }
});

const component = ref<HTMLButtonElement>();
const actualLayout = ref<ButtonLayout>(props.layout);

const componentStyles = computed(() => {
    let styles = ''

    // Add button type styles
    if (props.disabled == true) {
        styles += ' theme-action-disabled cursor-not-allowed';
    } 
    else {
        switch (props.type) {
            case ButtonType.Primary:
                styles += ' theme-action-primary';
                break;
            case ButtonType.Secondary:
                styles += ' theme-action-secondary';
                break;
            case ButtonType.Danger :
                styles += ' theme-action-danger';
                break;
            case ButtonType.Success :
                styles += ' theme-action-success';
                break;
        }
    }

    if (actualLayout.value === ButtonLayout.Rectangle)
        styles += ' theme-action-border-round action-layout';
    else if (actualLayout.value === ButtonLayout.Square)
        styles += ' theme-action-border-round aspect-square ';
    else if (actualLayout.value === ButtonLayout.Circle)
        styles += ' flex items-center justify-center action-circle-layout';
    

    switch (props.size) {
        case ActionComponentSize.Medium:
            styles += ' action-layout';
            break;
        case ActionComponentSize.Small:
            styles += ' action-small-layout';
            break;
        case ActionComponentSize.ExtraSmall:
            styles += ' action-extra-small-layout';
            break;
    }

    return styles;
});

const contentStyles = computed(() => {
    let styles = ' ';

    if (props.icon && props.text)
        styles += ' justify-around gap-2';
    else
        styles += ' justify-center';

    return styles;
});

</script>